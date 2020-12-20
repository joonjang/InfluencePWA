import { Component, Inject } from '@angular/core';
// import { HttpClient, HttpParams } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators, AbstractControl, AsyncValidatorFn } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { BaseFormComponent } from '../base.form.component';

import { Principle } from './principle';
import { PrincipleType } from './principleType';
import { PrincipleService } from './principle.service';
import { ApiResult } from '../base.service';

@Component({
  selector: 'app-principle-edit',
  templateUrl: './principle-edit.component.html',
  styleUrls: ['./principle-edit.component.css']
})
export class PrincipleEditComponent
  extends BaseFormComponent {

  // the view title
  title: string;

  // the form model
  form: FormGroup;

  // the principle object to edit or create
  principle: Principle;

  // the principle object id, as fetched from the active route:
  // It's NULL when we're adding a new principle,
  // and not NULL when we're editing an existing one.
  id?: number;

  // the principleTypes array for the select
  principleTypes: PrincipleType[];

  // Activity Log (for debugging purposes)
  activityLog: string = '';

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private principleService: PrincipleService) {
    super();
  }

  ngOnInit() {
    this.form = new FormGroup({
      law: new FormControl('', Validators.required),
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      principleTypeId: new FormControl('', Validators.required)
    }, null, this.isDupePrinciple());

    // react to form changes
    this.form.valueChanges
      .subscribe(val => {
        if (!this.form.dirty) {
          this.log("Form Model has been loaded.");
        }
        else {
          this.log("Form was updated by the user.");
        }
      });

    // react to changes in the form.law control
    this.form.get("law")!.valueChanges
      .subscribe(val => {
        if (!this.form.dirty) {
          this.log("Name has been loaded with initial values.");
        }
        else {
          this.log("Name was updated by the user.");
        }
      });

    this.loadData();
  }

  log(str: string) {
    this.activityLog += "["
      + new Date().toLocaleString()
      + "] " + str + "<br />";
  }

  loadData() {

    // load principleTypes
    this.loadPrincipleTypes();

    // retrieve the ID from the 'id'
    this.id = +this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      // EDIT MODE

      // fetch the principle from the server
      this.principleService.get<Principle>(this.id).subscribe(result => {
        this.principle = result;
        this.title = "Edit - " + this.principle.law;

        // update the form with the principle value
        this.form.patchValue(this.principle);
      }, error => console.error(error));
    }
    else {
      // ADD NEW MODE

      this.title = "Create a new Principle";
    }
  }

  loadPrincipleTypes() {
    // fetch all the principleTypes from the server
    this.principleService.getPrincipleTypes<ApiResult<PrincipleType>>(
      0,
      9999,
      "law",
      null,
      null,
      null,
    ).subscribe(result => {
      this.principleTypes = result.data;
    }, error => console.error(error));
  }

  onSubmit() {

    var principle = (this.id) ? this.principle : <Principle>{};

    principle.law = this.form.get("law").value;
    principle.title = this.form.get("title").value;
    principle.description = this.form.get("description").value;
    principle.principleTypeId = +this.form.get("principleTypeId").value;

    if (this.id) {
      // EDIT mode
      this.principleService
        .put<Principle>(principle)
        .subscribe(result => {

          console.log("Principle " + principle.id + " has been updated.");

          // go back to principles view
          this.router.navigate(['/principles']);
        }, error => console.log(error));
    }
    else {
      // ADD NEW mode
      this.principleService
        .post<Principle>(principle)
        .subscribe(result => {

          console.log("Principle " + result.id + " has been created.");

          // go back to principles view
          this.router.navigate(['/principles']);
        }, error => console.log(error));
    }
  }

  isDupePrinciple(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {
      var principle = <Principle>{};
      principle.id = (this.id) ? this.id : 0;
      principle.law = this.form.get("law").value;
      principle.title = this.form.get("title").value;
      principle.description = this.form.get("description").value;
      principle.principleTypeId = +this.form.get("principleTypeId").value;

      return this.principleService.isDupePrinciple(principle)
        .pipe(map(result => {
          return (result ? { isDupePrinciple: true } : null);
        }));
    }
  }
}
