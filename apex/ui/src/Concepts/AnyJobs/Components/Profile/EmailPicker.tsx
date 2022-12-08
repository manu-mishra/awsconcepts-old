import { IBasePickerSuggestionsProps, ITag, mergeStyles, TagPicker } from '@fluentui/react';
import { useId } from '@fluentui/react-hooks';
import { ProfileDocument } from '../../Model/ApplicantsModel';
const rootClass = mergeStyles({
  maxWidth: 500,
});

const pickerSuggestionsProps: IBasePickerSuggestionsProps = {
  suggestionsHeaderText: 'Suggested Email',
  noResultsFoundText: 'Not found',
};

export const EmailPicker = ({ analysis }: ProfileDocument) => {
  const pickerId = useId('inline-picker');
  let nameTags: ITag[] = [];
  if (analysis !== undefined) {
    var namesCollection = uniqByMap(analysis.filter(function (el) {
      return el.type === 'EMAIL';
    }));
    nameTags = namesCollection.map(item => ({ key: item.text, name: item.text}));

  }
  function uniqByMap<T>(array: T[]): T[] {
    const map = new Map();
    for (const item of array) {
      map.set(item, item);
    }
    return Array.from(map.values());
  }

  const listContainsTagList = (tag: ITag, tagList?: ITag[]) => {
    if (!tagList || !tagList.length || tagList.length === 0) {
      return false;
    }
    return tagList.some(compareTag => compareTag.key === tag.key);
  };

  const filterSuggestedTags = (filterText: string, tagList?: ITag[]): ITag[] => {
    const defaultTags: ITag[] = [];
    defaultTags.push({ key: filterText, name: filterText });
    const result = filterText
      ? nameTags.filter(
        tag => tag.name.toLowerCase().indexOf(filterText.toLowerCase()) === 0 && !listContainsTagList(tag, tagList),
      )
      : [];
      console.log(nameTags);
    console.log(result);
    if (result.length === 0)
      return defaultTags;
    return result;
  };

  const getTextFromItem = (item: ITag) => item.name;
  return (

    <>
      <div className={rootClass}>
        <label htmlFor={pickerId}>Email</label>
        <TagPicker
          removeButtonAriaLabel="Remove"
          selectionAriaLabel="Selected Email"

          onResolveSuggestions={filterSuggestedTags}
          getTextFromItem={getTextFromItem}
          pickerSuggestionsProps={pickerSuggestionsProps}
          itemLimit={1}
          // this option tells the picker's callout to render inline instead of in a new layer
          pickerCalloutProps={{ doNotLayer: true }}
          inputProps={{
            id: pickerId,
          }}
        />
      </div>
    </>
  )
}
