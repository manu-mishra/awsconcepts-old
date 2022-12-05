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
  text?: string;
  size?: number;
  analysis?: ProfileDocumentAnalysis[];
}
export interface ProfileDocumentAnalysis {
  beginOffset?: number;
  endOffset?: number;
  score?: number;
  text?: string;
  type?: string;

}