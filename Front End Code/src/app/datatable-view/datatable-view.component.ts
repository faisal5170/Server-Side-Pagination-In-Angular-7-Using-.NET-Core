import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { User } from '../models/user';
import { SearchCriteria } from '../models/search-criteria';
import { Subject, Observable, Subscription } from 'rxjs';
import { AppService } from '../app.service';
import { DataTableDirective } from "angular-datatables";
import { timer } from 'rxjs';

@Component({
  selector: 'app-datatable-view',
  templateUrl: './datatable-view.component.html',
  styleUrls: ['./datatable-view.component.css']
})
export class DatatableViewComponent implements OnInit {

  title = "app";
  users: User[];
  userName: string;
  searchCriteria: SearchCriteria = { isPageLoad: true, filter: "" };

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();

  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;

  timerSubscription: Subscription;

  constructor(private appService: AppService) {}

  ngOnInit() {
    this.dtOptions = {
      pagingType: "full_numbers",
      pageLength: 10,
      serverSide: true,
      processing: true,
      searching: false,
      ajax: (dataTablesParameters: any, callback) => {
        dataTablesParameters.searchCriteria = this.searchCriteria;
        this.appService
          .getAllEmployeesWithPaging(dataTablesParameters)
          .subscribe(resp => {
            this.users = resp.data;

            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: []
            });
          });
      },
      columns: [null, null, null, null, { orderable: false }]
    };

    this.subscribeToData();
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next();    
  }

  rerender(): void {
    this.searchCriteria.isPageLoad = false;
    this.searchCriteria.filter = this.userName;
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();
      this.dtTrigger.next();
    });
  }

  search() {
    this.rerender();
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
   
    if (this.timerSubscription) {
      this.timerSubscription.unsubscribe();
    }
  }

  private refreshData(): void {
    this.rerender();
    this.subscribeToData();    
  }

  private subscribeToData(): void {
    this.refreshData();
  }

}
