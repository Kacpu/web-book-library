import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormControl} from "@angular/forms";
import {map, Observable, startWith} from "rxjs";
import {Searchable} from "../../interfaces/searchable";

@Component({
  selector: 'app-autocomplete-search-bar',
  templateUrl: './autocomplete-search-bar.component.html',
  styleUrls: ['./autocomplete-search-bar.component.css']
})
export class AutocompleteSearchBarComponent implements OnInit {

  @Input() options: Searchable[] = [];
  @Input() searchLabel: string = 'Search';

  @Output() searchResultChange = new EventEmitter<Searchable[]>();

  searchControl = new FormControl<string | Searchable>('');
  filteredOptionsObservable!: Observable<Searchable[]>;
  filteredValues: Searchable[] = [];

  ngOnInit() {
    this.filteredOptionsObservable = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(value => {
        const filterValue = typeof value === 'string'? value : value?.searchValue;
        this.filteredValues = filterValue ? this._filter(filterValue) : this.options;

        if(value === '') {
          this.searchResultChange.emit(this.filteredValues);
        }

        return filterValue ? this.filteredValues : [];
      }),
    );
  }

  private _filter(value: string): Searchable[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.searchValue?.toLowerCase().includes(filterValue));
  }

  displayFn(option: Searchable) {
    return option && option.searchValue ? option.searchValue : '';
  }

  onSubmit(event: Event) {
    event.preventDefault();

    this.searchResultChange.emit(this.filteredValues);
    // this.searchControl.setValue('');
  }
}
