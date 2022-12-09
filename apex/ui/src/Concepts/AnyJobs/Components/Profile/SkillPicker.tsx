import { IBasePickerSuggestionsProps, ITag, mergeStyles, TagPicker } from '@fluentui/react';
import { useId } from '@fluentui/react-hooks';
import { ProfilePickerArguments } from '../../Model/ApplicantsModel';
const rootClass = mergeStyles({
  maxWidth: 500,
});

const pickerSuggestionsProps: IBasePickerSuggestionsProps = {
  suggestionsHeaderText: 'Suggested Skill',
  noResultsFoundText: 'Not found',
};

export const SkillPicker = ({ analysis, profileDraft, handleChange }: ProfilePickerArguments) => {
  const pickerId = useId('inline-picker');
  let selectedTags: ITag[] = [];
  if (profileDraft && profileDraft.skills)
    selectedTags = profileDraft.skills.map(item => ({ key: item, name: item }));
  let skillTags: ITag[] = [];
  if (analysis !== undefined) {
    var namesCollection = uniqByMap(analysis.filter(function (el) {
      return el.type === 'TITLE';
    }));
    skillTags = namesCollection.map(item => ({ key: item.text, name: item.text }));

  }
  function uniqByMap<T>(array: T[]): T[] {
    const map = new Map();
    for (const item of array) {
      map.set(item, item);
    }
    return Array.from(map.values());
  }
  function onChange(items?: any[]): void {
    if (items) {
      let selectedItems: string[] = [];
      items.forEach((item) => { selectedItems.push(item.name) })
      handleChange(selectedItems);
    }

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
      ? skillTags.filter(
        tag => tag.name.toLowerCase().indexOf(filterText.toLowerCase()) === 0 && !listContainsTagList(tag, tagList),
      )
      : [];
    if (result.length === 0)
      return defaultTags;
    return result;
  };

  const getTextFromItem = (item: ITag) => item.name;
  return (

    <>
      <div className={rootClass}>
        <label htmlFor={pickerId}>Skills</label>
        <TagPicker
          onChange={onChange}
          removeButtonAriaLabel="Remove"
          selectionAriaLabel="Selected colors"
          onResolveSuggestions={filterSuggestedTags}
          getTextFromItem={getTextFromItem}
          pickerSuggestionsProps={pickerSuggestionsProps}
          itemLimit={10}
          defaultSelectedItems={ selectedTags}
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
