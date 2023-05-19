import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  HospitalServiceProxy,
  HospitalDto,
  HospitalDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateHospitalDialogComponent } from './create-hospital/create-hospital-dialog.component';
import { EditHospitalDialogComponent } from './edit-hospital/edit-hospital-dialog.component';

class PagedHospitalsRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './hospital.component.html',
  animations: [appModuleAnimation()]
})
export class HospitalsComponent extends PagedListingComponentBase<HospitalDto> {
  hospitals: HospitalDto[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _hospitalService: HospitalServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  createHospital(): void {
    this.showCreateOrEditHospitalDialog();
  }

  editHospital(hospital: HospitalDto): void {
    this.showCreateOrEditHospitalDialog(hospital.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedHospitalsRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = this.isActive;

    this._hospitalService
        .getAll(request.keyword ,"","","",
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: HospitalDtoPagedResultDto) => {
        this.hospitals = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  protected delete(hospital: HospitalDto): void {
      abp.message.confirm(
          this.l('HospitalDeleteWarningMessage', hospital.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._hospitalService.delete(hospital.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditHospitalDialog(id?: number): void {
    let createOrEditHospitalDialog: BsModalRef;
    if (!id) {
      createOrEditHospitalDialog = this._modalService.show(
        CreateHospitalDialogComponent,
        {
          class: 'modal-lg',
          ignoreBackdropClick:true
        }
      );
    } else {
      createOrEditHospitalDialog = this._modalService.show(
        EditHospitalDialogComponent,
        {
          class: 'modal-lg',
          ignoreBackdropClick:true,
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditHospitalDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
