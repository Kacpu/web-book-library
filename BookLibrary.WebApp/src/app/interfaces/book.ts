import {Searchable} from "./searchable";

export interface Book extends Searchable{
  title: string;
  url: string;
}
