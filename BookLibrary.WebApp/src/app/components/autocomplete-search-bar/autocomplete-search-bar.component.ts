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
  @Input() options$!: Observable<Searchable[]>;

  @Output() fetchOptions = new EventEmitter<string>();
  @Output() search = new EventEmitter<string>();

  searchControl = new FormControl<string | Searchable>('');
  filterValue: string = '';

  optionsFiltered$!: Observable<Searchable[]>;
  @Input() options: Searchable[] | undefined;

  subscription: Subscription = new Subscription();

  ngOnInit(): void {
    this.optionsFiltered$ = this.searchControl.valueChanges.pipe(
      startWith(this.filterValue),
      tap(value => {
        //console.log("onchange " + this.searchControl.value)

        //this.filterValue = typeof value === 'string' ? value : value?.searchValue ? value.searchValue : '';

        // if (this.filterValue?.length === 3) {
        //   this.fetchOptions.emit(this.filterValue);
        // }

        // if (this.filterValue === '') {
        //   this.search.emit(this.filterValue);
        // }
      }),
      map(value => {
        return this.filter()
      })
    );

    this.subscription.add(this.searchControl.valueChanges.subscribe(value => {
      // console.log(this.options)

      // console.log("onchange " + this.searchControl.value)

      this.filterValue = typeof value === 'string' ? value : value?.searchValue ? value.searchValue : '';

      if (this.filterValue?.length === 3) {
        this.fetchOptions.emit(this.filterValue);
        this.options = undefined;
        console.log(this.options)
      }

      if (this.filterValue === '') {
        this.search.emit(this.filterValue);
      }
    }));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  onSearchControlChange() {
    // console.log("onchange " + this.searchControl.value)
    //
    // const value = this.searchControl.value;
    // this.filterValue = typeof value === 'string' ? value : value?.searchValue ? value.searchValue : '';
    //
    // if (this.filterValue && this.filterValue.length === 1) {
    //   this.fetchOptions.emit(this.filterValue);
    // }
    //
    // if (this.filterValue === '') {
    //   this.search.emit(this.filterValue);
    // }
  }

  // private _filter(value: string): Searchable[] {
  //   const filterValue = value.toLowerCase();
  //
  //   return this.options!.filter(option => option.searchValue?.toLowerCase().includes(filterValue));
  // }

  // filter(options: Searchable[]): Searchable[] {
  //   console.log("filter " + this.filterValue)
  //   if (this.filterValue.length > 2) {
  //     return options.filter(option => option.searchValue?.toLowerCase().includes(this.filterValue.toLowerCase()));
  //   } else {
  //     return [];
  //   }


  filter(): Searchable[] {
    console.log("filter " + this.filterValue)
    if (this.options && this.filterValue.length > 2) {
      return this.options.filter(option => option.searchValue?.toLowerCase().includes(this.filterValue.toLowerCase()));
    } else {
      return [];
    }
  }

  displayFn(option: Searchable) {
    return option && option.searchValue ? option.searchValue : '';
  }

  onSubmit(event: Event) {
    event.preventDefault();
    console.log(this.searchControl.value)
    this.search.emit(this.filterValue);
    // this.searchControl.setValue('');
  }
}
