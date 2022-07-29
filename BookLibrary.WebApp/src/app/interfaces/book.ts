import {Searchable} from "./searchable";

export interface Book {
  id: number,
  title: string;
  releaseYear: number | undefined;
  description: string;
  language: string;
  numberOfPages: number;
  authorId: number;
  author: string;
  publisherId: number;
  publisherName: string;
  bookSeriesId: number | undefined;
  bookSeriesName: string;
}

export interface BookShortData extends Searchable {
  title: string;
}
