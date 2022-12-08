import { IBasePickerSuggestionsProps, ITag, mergeStyles, TagPicker } from '@fluentui/react';
import { useId } from '@fluentui/react-hooks';
const rootClass = mergeStyles({
    maxWidth: 500,
  });
  
  const pickerSuggestionsProps: IBasePickerSuggestionsProps = {
    suggestionsHeaderText: 'Suggested colors',
    noResultsFoundText: 'No color tags found',
  };
  
  const testTags: ITag[] = [
    'black',
    'blue',
    'brown',
    'cyan',
    'green',
    'magenta',
    'mauve',
    'orange',
    'pink',
    'purple',
    'red',
    'rose',
    'violet',
    'white',
    'yellow',
  ].map(item => ({ key: item, name: item[0].toUpperCase() + item.slice(1) }));
  
  const listContainsTagList = (tag: ITag, tagList?: ITag[]) => {
    if (!tagList || !tagList.length || tagList.length === 0) {
      return false;
    }
    return tagList.some(compareTag => compareTag.key === tag.key);
  };

  const filterSuggestedTags = (filterText: string, tagList?: ITag[]): ITag[] => {
    const defaultTags:ITag[]=[];
    defaultTags.push({ key: filterText, name: filterText });
    console.log(defaultTags);
    const result= filterText
      ? testTags.filter(
          tag => tag.name.toLowerCase().indexOf(filterText.toLowerCase()) === 0 && !listContainsTagList(tag, tagList),
        )
      :  [];
      console.log(result);
      if(result.length===0)
        return defaultTags;
      return result;
  };
  
  const getTextFromItem = (item: ITag) => item.name;
export const SkillPicker = () => {
    const pickerId = useId('inline-picker');

  return (
    
    <>
    <div className={rootClass}>
      <label htmlFor={pickerId}>Choose Skills</label>
      <TagPicker
        removeButtonAriaLabel="Remove"
        selectionAriaLabel="Selected colors"
        
        onResolveSuggestions={filterSuggestedTags}
        getTextFromItem={getTextFromItem}
        pickerSuggestionsProps={pickerSuggestionsProps}
        itemLimit={10}
        // this option tells the picker's callout to render inline instead of in a new layer
        pickerCalloutProps={{ doNotLayer: true }}
        inputProps={{
          id: pickerId,
        }}
      />
      <div
        // since this example is an inline picker, it needs some forced space below
        // so when embedded in the docssite, the dropdown shows up
        //  style={{ height: '10em' }}
      />
    </div>
    </>
  )
}
