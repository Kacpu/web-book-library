import {Component, OnDestroy, OnInit} from '@angular/core';
import {Book, BookShortData} from "../../interfaces/book";
import {BookService} from "../../services/book.service";
import {Observable, Subscription} from "rxjs";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit, OnDestroy {

  // books$!: Observable<Book[]>;
  // books: Book[] = [];
  loadedBooks: Book[] = [];
  readonly chunkToLoad = 8;
  isLoading = true;
  isLoadingDisabled = false;

  titleFilter = '';

  bookSearchOptions: BookShortData[] | undefined;
  // bookSearchOptions$!: Observable<BookShortData[]>;

  subscriptions: Subscription = new Subscription();

  constructor(private bookService: BookService) {
  }

  ngOnInit(): void {
    // this.books$ = this.bookService.getBooks('');
    this.initList();
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  initList() {
    this.loadedBooks = [];
    this.isLoadingDisabled = false;
    this.loadBooks();
  }

  loadBooks() {
    this.isLoading = true;
    const skip = this.loadedBooks.length;
    // const end = skip + this.chunkToLoad;

    const sub = this.bookService.getBooks(this.titleFilter, skip, this.chunkToLoad)
      .subscribe(books => {
        // this.books = books;
        // this.loadedBooks.push(...this.books.slice(skip, end));

        if(books.length === 0){
          this.isLoadingDisabled = true;
        }

        this.loadedBooks.push(...books);
        this.isLoading = false;
      });

    this.subscriptions.add(sub);
  }

  onSearch(filter: string) {
    // this.books$ = this.bookService.getBooks(filter);
    this.titleFilter = filter;
    this.initList();
  }

  onScroll() {
    console.log("hi there");
    this.loadBooks();
  }

  onFetchOptions(filter: string) {
    const sub = this.bookService.getBooksShortData(filter)
      .subscribe(options => this.bookSearchOptions = options);

    this.subscriptions.add(sub);

    // this.bookSearchOptions$ = this.bookService.getBooksShortData(filter);
  }
}
