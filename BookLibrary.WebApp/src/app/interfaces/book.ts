import {Searchable} from "./searchable";

export interface Book {
  id: number;
  title: string;
  releaseYear: number | null;
  description: string;
  language: string;
  numberOfPages: number;
  authorId: number;
  author: string;
  publisherId: number;
  publisherName: string;
  bookSeriesId: number | null
  bookSeriesName: string | null;
}

export interface BookShortData extends Searchable {
  id: number;
  title: string;
}
