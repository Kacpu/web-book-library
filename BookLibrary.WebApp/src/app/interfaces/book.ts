import {Searchable} from "./searchable";

export interface Book extends Searchable {
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
