import {Component, OnDestroy, OnInit} from '@angular/core';
import {Book, BookShortData} from "../../interfaces/book";
import {BookService} from "../../services/book.service";
import {Subscription} from "rxjs";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit, OnDestroy {

  loadedBooks: Book[] = [];
  readonly chunkToLoad = 8;
  isLoading = false;
  isLoadingDisabled = false;

  titleFilter = '';

  bookSearchOptions: BookShortData[] | undefined;

  subscriptions: Subscription = new Subscription();

  constructor(private bookService: BookService) {}

  ngOnInit(): void {
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
    if (this.isLoading) {
      return;
    }

    this.isLoading = true;
    const skip = this.loadedBooks.length;

    const sub = this.bookService.getBooks(this.titleFilter, skip, this.chunkToLoad)
      .subscribe(books => {
        if(books.length === 0) {
          this.isLoadingDisabled = true;
        }

        this.loadedBooks.push(...books);
        this.isLoading = false;
      });

    this.subscriptions.add(sub);
  }

  onSearch(filter: string) {
    this.titleFilter = filter;
    this.initList();
  }

  onScroll() {
    this.loadBooks();
  }

  onFetchOptions(filter: string) {
    const sub = this.bookService.getBooksShortData(filter)
      .subscribe(options => {this.bookSearchOptions = options
        console.log(options)});

    this.subscriptions.add(sub);
  }
}
