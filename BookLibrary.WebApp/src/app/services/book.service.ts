import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Book} from "../interfaces/book";
import {catchError, map, retry} from 'rxjs/operators';
import {throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BookService {

  serverUrl = 'https://localhost:5001';

  constructor(private http: HttpClient) { }

  getBooks() {
    return this.http.get<Book[]>(this.serverUrl + '/book')
      .pipe(
        map(this._mapper),
        retry(3),
        catchError(this._handleError)
      );
  }

  private _mapper = (res: Book[]): Book[] => res.map(book => {
    return {
      id: book.id,
      title: book.title,
      author: book.author,
      authorId: book.authorId,
      bookSeriesId: book.bookSeriesId,
      bookSeriesName: book.bookSeriesName,
      description: book.description,
      language: book.language,
      numberOfPages: book.numberOfPages,
      publisherId: book.publisherId,
      publisherName: book.publisherName,
      releaseYear: book.releaseYear,
      searchValue: book.title,
    };
  });

  private _handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      console.error('An error occurred:', error.error);
    } else {
      console.error(
        `Backend returned code ${error.status}, body was: `, error.error);
    }
    return throwError(() => new Error('Something bad happened; please try again later.'));
  }
}