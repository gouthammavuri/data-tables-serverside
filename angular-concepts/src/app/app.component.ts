import { HttpClient } from '@angular/common/http';
import { OnChanges, OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { DataTablesParameters } from './models/datatables-parameters';
import { DataTablesResponse } from './models/datatables-response';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'angular-concepts';

  dtOptions: DataTables.Settings = {};
  pageSize: number = 10;
  pageNumber: number = 1;
  searchText: string = '';
  employees: any[] = [];
  totalRecords: number = 0;

  constructor(private http: HttpClient) { }
  
  ngOnInit() {
    var that = this;
    let table = $('#dtex').DataTable({
      pagingType: 'full',
        pageLength: this.pageSize,
        responsive: true,
        processing: true,
        serverSide: true,
        ajax: (dataTablesParameters: any, callback) => {
          that.getEmployeeFromAPI(this.pageNumber, this.pageSize, this.searchText);
          callback({
                    recordsTotal: this.totalRecords,
                    recordsFiltered: 0,
                    data: []
                });
      },
      columns: [
          { data: "id" },
          { data: "name" },
          { data: "email" },
          { data: "designation" }
      ],
      drawCallback: () => {
        $('.paginate_button.first').on('click', () => {
            this.firstButtonClickEvent();
        });
        $('.paginate_button.previous').on('click', () => {
          this.previousButtonClickEvent();
        });
        $('.paginate_button.next').on('click', () => {
          this.nextButtonClickEvent();
        });
        $('.paginate_button.last').on('click', () => {
          this.lastButtonClickEvent();
        });
        $('select[name="dtex_length"]').on('change', () => {
          this.pageSizeChangeEvent();
        });
        $('.dataTables_filter').find("input[type=search]").first().on("keydown", (event) => {
          let searchValueText: string = $('#dtex_filter').find('input[type=search]').first().val() as string;
          // enter
          if(event.keyCode === 13) {
            that.searchText = searchValueText;
            that.getEmployeeFromAPI(that.pageNumber, that.pageSize, that.searchText);
          }
          // back space
          if(event.keyCode === 8) {
            that.searchText = searchValueText;
            that.getEmployeeFromAPI(that.pageNumber, that.pageSize, that.searchText);
          }
        });
      },
    });
  }

  firstButtonClickEvent(): void {
    this.pageNumber = 1;
    this.getEmployeeFromAPI(this.pageNumber, this.pageSize, this.searchText);
  }

  previousButtonClickEvent(): void {
    if(this.pageNumber > 1) {
      this.pageNumber = this.pageNumber - 1;
    }
    this.getEmployeeFromAPI(this.pageNumber, this.pageSize, this.searchText);
  }

  nextButtonClickEvent(): void {
    if(this.totalRecords/this.pageSize > this.pageNumber) {
      this.pageNumber = this.pageNumber + 1;
    }
    this.getEmployeeFromAPI(this.pageNumber, this.pageSize, this.searchText);
  }

  lastButtonClickEvent(): void {
    if(this.totalRecords % this.pageSize == 0) {
      this.pageNumber = this.totalRecords/this.pageSize;
    } else if (this.totalRecords % this.pageSize > 0) {
      this.pageNumber = Math.floor(this.totalRecords/this.pageSize) + 1
    }
    this.getEmployeeFromAPI(this.pageNumber, this.pageSize, this.searchText);
  }

  pageSizeChangeEvent(): void {
    let dp = $('select[name="dtex_length"]');
    // let selectedIndex = dp.prop('selectedIndex'); -- Get SelectedIndex
    this.pageNumber = 1;
    this.pageSize = dp.val() as number;
    this.getEmployeeFromAPI(this.pageNumber, this.pageSize, this.searchText);
  }

  getEmployeeFromAPI(pageNumber: number, pageSize: number, searchText: string) {
    let dataTablesParameters = {
      pageNumber: pageNumber,
      pageSize: pageSize,
      searchText: searchText
    };
    this.http.post<DataTablesResponse>('https://localhost:5001/employee/serversidepagination', dataTablesParameters, {})
      .subscribe(resp => {
        this.employees = resp.data;
        this.totalRecords = resp.totalRecords;
    });
  }
}
