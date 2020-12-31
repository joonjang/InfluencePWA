import { Component } from '@angular/core';

import { Principle } from '../quote-data/principle';
import { PrincipleType } from '../quote-data/principleType';
import { PrincipleService } from '../quote-data//principle.service';
import { ApiResult } from '../base.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  public principle: Principle = { id: 0, law: 'Test1', title: 'test2', description: 'test3', principleTypeId: 0, principleTypeName: 'test4' } ;

  // Activity Log (for debugging purposes)
  activityLog: string = '';

  // the city object id, as fetched from the active route:
  // It's NULL when we're adding a new city,
  // and not NULL when we're editing an existing one.
  //id?: number;

  constructor(
    private principleService: PrincipleService) {
  }


  ngOnInit() {
    this.getData();
  }

  log(str: string) {
    this.activityLog += "["
      + new Date().toLocaleString()
      + "] " + str + "<br />";
  }


  getData() {

    this.principleService.get<Principle>(1).subscribe(result => {
      this.principle.principleTypeName = result.principleTypeName;
      this.principle.law = result.law;
      this.principle.title = result.title;
      this.principle.description = result.description;
    }, error => console.error(error));
  }
  //  Data<ApiResult<Principle>>()
  //    .subscribe(result => {
  //      this.paginator.length = result.totalCount;
  //      this.paginator.pageIndex = result.pageIndex;
  //      this.paginator.pageSize = result.pageSize;
  //      this.principles = new MatTableDataSource<Principle>(result.data);
  //    }, error => console.error(error));
  //}
  
}
