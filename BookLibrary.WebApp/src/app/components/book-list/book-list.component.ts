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
  bookSearchOptions$!: Observable<BookShortData[]>;

  options: BookShortData[] | undefined;

  constructor(private bookService: BookService) { }

  ngOnInit(): void {
    this.books$ = this.bookService.getBooks('');
  }

  onFetchOptions(filter: string) {
    this.bookSearchOptions$ = this.bookService.getBooksShortData(filter);

    this.bookService.getBooksShortData(filter).subscribe(value => this.options = value)
  }

  onSearch(filter: string) {
    console.log("book filter " + filter);
    this.books$ = this.bookService.getBooks(filter);
  }

  // toBooks(objects: object[]) {
  //   if(objects.length > 0 && "title" in objects[0]) {
  //     return objects as Book[];
  //   } else {
  //     return [];
  //   }
  // }
}
