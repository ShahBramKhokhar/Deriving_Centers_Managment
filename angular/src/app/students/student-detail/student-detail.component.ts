import { ActivatedRoute } from "@angular/router";
import { Component, Injector, OnInit } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import {
  CentreDto,
  CenterServiceProxy,
} from "@shared/service-proxies/service-proxies";
import { BsModalService } from "ngx-bootstrap/modal";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PrinterService } from "@shared/helpers/printer.service";

@Component({
  selector: "app-student-detail",
  templateUrl: "./student-detail.component.html",
  styleUrls: ["./student-detail.component.css"],
  animations: [appModuleAnimation()],
})
export class StudentDetailComponent extends AppComponentBase implements OnInit {
  studentId: number;
  centerDetail: CentreDto = new CentreDto();
  constructor(
    private rout: ActivatedRoute,
    injector: Injector,
    private _modalService: BsModalService,
    private _centerService: CenterServiceProxy,
    private printer: PrinterService

  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.rout.queryParams.subscribe((params) => {
      if (params.studentId) {
        this.studentId = Number(params.studentId);
        console.log('this.studentId in detail',this.studentId);
        // this._centerService.getById(this.centerId).subscribe(res=>{
        //     this.centerDetail = res;
        // });
      }
    });
  }

  public print() {
    let style = `
    table#target-table{
      width:100%;
      border-collapse: collapse;
    }
    table#target-table td,th{
      border: 1px solid black;
    }
    `
    this.printer.print(document.getElementById("printableDiv").innerHTML, style, "Fees Detail", "landscape", 'A4', true);
  }
}
