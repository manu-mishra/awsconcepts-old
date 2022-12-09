export interface ProfileSummary {
  id: string;
  name: string;
  title: string;
  profileHighlights: string;
}
export interface ProfileDraft {
  id?: string;
  name?: string;
  title?: string;
  profileHighlights?: string;
  profileText?: string;
  skills?:string[],
  profileDocumentId?:string;
}
export interface ProfileDocument {
  id?: string;
  name?: string;
  documentText?: string;
  size?: number;
  analysis?: ProfileDocumentAnalysis[];
}
export interface ProfileDocumentSummary {
  id?: string;
  name?: string;
}
export interface ProfileDocumentAnalysis {
  beginOffset: number;
  endOffset: number;
  score: number;
  text: string;
  type: string;

}

export interface ProfilePickerArguments {
  analysis: ProfileDocumentAnalysis[]| undefined;
  profileDraft: ProfileDraft| undefined
  handleChange(selectedItems: string[]| undefined): void;
}