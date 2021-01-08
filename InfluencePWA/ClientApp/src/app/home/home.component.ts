import { Component } from '@angular/core';

import { Principle } from '../quote-data/principle';
import { PrincipleType } from '../quote-data/principleType';
import { PrincipleService } from '../quote-data//principle.service';
import { ApiResult } from '../base.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  public principle: Principle = { id: 0, law: '-', title: '-', description: '-', principleTypeId: 0, principleTypeName: '-' } ;

  // Activity Log (for debugging purposes)
  activityLog: string = '';

  constructor(
    private principleService: PrincipleService) {
  }


  ngOnInit() {
    this.newRandomPrinciple();
  }

  log(str: string) {
    this.activityLog += "["
      + new Date().toLocaleString()
      + "] " + str + "<br />";
  }


  getData(num: number) {

    this.principleService.get<Principle>(num).subscribe(result => {
      this.principle.principleTypeId = result.principleTypeId;
      this.principle.law = result.law;
      this.principle.title = result.title;
      this.principle.description = result.description;
      this.principleService.getPrincipleType<PrincipleType>(this.principle.principleTypeId).subscribe(result2 => {
        this.principle.principleTypeName = result2.name;
      }, error => console.error(error));
    }, error => console.error(error));
    
  }

  public newRandomPrinciple() {
    this.getData(Math.floor(Math.random() * 147) + 1);
  }
  
}
