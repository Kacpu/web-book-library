import {Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Book, BookShortData} from "../interfaces/book";
import {catchError, map, retry, shareReplay} from 'rxjs/operators';
import {throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BookService {

  serverUrl = 'https://localhost:5001';

  constructor(private http: HttpClient) {}

  getBook(id: string) {
    return this.http.get<Book>(this.serverUrl + `/book/${id}`)
      .pipe(
        shareReplay(),
        map(this._mapper),
        retry(3),
        catchError(this._handleError)
      );
  }

  getBooks(title: string = '', skip: number | '' = '', take: number | '' = '') {
    return this.http.get<Book[]>(this.serverUrl + `/book?title=${title}&skip=${skip}&take=${take}`)
      .pipe(
        shareReplay(),
        map(res => res.map(this._mapper)),
        retry(3),
        catchError(this._handleError)
      );
  }

  getBooksShortData(title: string = '') {
    return this.http.get<BookShortData[]>(this.serverUrl + `/book?title=${title}&isShort=true`)
      .pipe(
        shareReplay(),
        map(res => res.map(this._shortDataMapper)),
        retry(3),
        catchError(this._handleError)
      );
  }

  private _mapper = (book: Book): Book => {
    return {
      ...book
    };
  };

  private _shortDataMapper = (bookShort: BookShortData): BookShortData => {
    return {
      id: bookShort.id,
      title: bookShort.title,
      searchValue: bookShort.title
    };
  };

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
