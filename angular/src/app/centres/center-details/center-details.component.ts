import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateStudentComponent } from '@app/students/create-student/create-student.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { CenterServiceProxy, CentreDto } from '@shared/service-proxies/service-proxies';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-center-details',
  templateUrl: './center-details.component.html',
  styleUrls: ['./center-details.component.css'],
  animations: [appModuleAnimation()],
})
export class CenterDetailsComponent extends  AppComponentBase  implements OnInit {

  centerId :number;
  centerDetail :CentreDto = new CentreDto();
  constructor(
    private rout: ActivatedRoute,
    injector: Injector,
    private _modalService: BsModalService,
    private _centerService:CenterServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.rout.queryParams.subscribe(params => {

      if (params.centerId) {
        this.centerId = Number(params.centerId);

        this._centerService.getById(this.centerId).subscribe(res=>{
            this.centerDetail = res;
        });
      }
    });

  }
 
        
        }
      
