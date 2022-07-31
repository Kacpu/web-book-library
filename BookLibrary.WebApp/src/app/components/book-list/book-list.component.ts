import {Component, OnInit} from '@angular/core';
import {Book, BookShortData} from "../../interfaces/book";
import {BookService} from "../../services/book.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit {

  books$!: Observable<Book[]>;

  bookSearchOptions: BookShortData[] | undefined;
  // bookSearchOptions$!: Observable<BookShortData[]>;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.books$ = this.bookService.getBooks('');
  }

  onFetchOptions(filter: string) {
    this.bookService.getBooksShortData(filter).subscribe(value => this.bookSearchOptions = value);
    // this.bookSearchOptions$ = this.bookService.getBooksShortData(filter);
  }

  onSearch(filter: string) {
    this.books$ = this.bookService.getBooks(filter);
  }
}
