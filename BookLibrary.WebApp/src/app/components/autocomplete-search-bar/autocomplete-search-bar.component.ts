import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {FormControl} from "@angular/forms";
import {map, Observable, startWith, Subscription, tap} from "rxjs";
import {Searchable} from "../../interfaces/searchable";

@Component({
  selector: 'app-autocomplete-search-bar',
  templateUrl: './autocomplete-search-bar.component.html',
  styleUrls: ['./autocomplete-search-bar.component.css']
})
export class AutocompleteSearchBarComponent implements OnInit, OnDestroy {

  @Input() searchLabel: string = 'Search';
  @Input() options: Searchable[] | undefined;

  @Output() fetchOptions = new EventEmitter<string>();
  @Output() search = new EventEmitter<string>();

  searchControl = new FormControl<string | Searchable>('');
  filterValue: string = '';

  // filteredOptions: Searchable[] = [];
  filteredOptions$!: Observable<Searchable[]>;

  subscriptions: Subscription = new Subscription();

  ngOnInit(): void {
    this.subscriptions.add(
      this.searchControl.valueChanges.subscribe(value => {
        this.filterValue = typeof value === 'string' ? value : value?.searchValue ? value.searchValue : '';

        if (this.filterValue.length === 3) {
          if (this.filterValue.trim().length >= 3) {
            this.fetchOptions.emit(this.filterValue);
            this.options = undefined;
          }
          else {
            this.options = [];
          }
        }
        else if (this.filterValue === '') {
          this.search.emit(this.filterValue);
        }
        // else if (this.filterValue.length > 3){
        //   this.filter();
        // }
      })
    );

    this.filteredOptions$ = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(() => {
        return this.filter()
      })
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  filter() {
    if (this.options) {
      return this.options.filter(option => option.searchValue?.toLowerCase()
        .includes(this.filterValue.toLowerCase()));
    } else {
      return [];
    }
  }

  // filter() {
  //   if (this.options) {
  //     this.filteredOptions = this.options.filter(option => option.searchValue?.toLowerCase()
  //       .includes(this.filterValue.toLowerCase()));
  //   } else {
  //     this.filteredOptions = [];
  //   }
  // }

  displayFn(option: Searchable) {
    return option && option.searchValue ? option.searchValue : '';
  }

  onSubmit(event: Event) {
    event.preventDefault();
    this.search.emit(this.filterValue);
  }
}
