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
