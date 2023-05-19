import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  MenuItemServiceProxy,
  MenuItemDto,
  MenuItemDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateMenuItemDialogComponent } from './create-menuitem/create-menuitem-dialog.component';
import { EditMenuItemDialogComponent } from './edit-menuitem/edit-menuitem-dialog.component';

class PagedMenuItemsRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './menuitems.component.html',
  animations: [appModuleAnimation()]
})
export class MenuItemsComponent extends PagedListingComponentBase<MenuItemDto> {
  menuitems: MenuItemDto[] = [];
  keyword = '';
  isActive: boolean | null;
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _menuitemService: MenuItemServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  createMenuItem(): void {
    this.showCreateOrEditMenuItemDialog();
  }

  editMenuItem(menuitem: MenuItemDto): void {
    this.showCreateOrEditMenuItemDialog(menuitem.id);
  }

  clearFilters(): void {
    this.keyword = '';
    this.isActive = undefined;
    this.getDataPage(1);
  }

  protected list(
    request: PagedMenuItemsRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;
    request.isActive = this.isActive;

    this._menuitemService
      .getAll(
        request.keyword,
        request.isActive,
        request.skipCount,
        request.maxResultCount
      )
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: MenuItemDtoPagedResultDto) => {
        this.menuitems = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  protected delete(menuitem: MenuItemDto): void {
    abp.message.confirm(
      this.l('MenuItemDeleteWarningMessage', menuitem.lable),
      undefined,
      (result: boolean) => {
        if (result) {
          this._menuitemService.delete(menuitem.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditMenuItemDialog(id?: number): void {
    let createOrEditMenuItemDialog: BsModalRef;
    if (!id) {
      createOrEditMenuItemDialog = this._modalService.show(
        CreateMenuItemDialogComponent,
        {
          class: 'modal-lg',
          ignoreBackdropClick:true
        }
      );
    } else {
      createOrEditMenuItemDialog = this._modalService.show(
        EditMenuItemDialogComponent,
        {
          class: 'modal-lg',
          ignoreBackdropClick:true,
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditMenuItemDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
