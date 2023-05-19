import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import {
  MenuItemServiceProxy,
  MenuItemDto,
  RoleDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './edit-menuitem-dialog.component.html'
})
export class EditMenuItemDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  menuitem = new MenuItemDto();
  roles: RoleDto[] = [];
  checkedRolesMap: { [key: string]: boolean } = {};
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _menuitemService: MenuItemServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._menuitemService.get(this.id).subscribe((result) => {
      this.menuitem = result;
    });
  }

  save(): void {
    this.saving = true;

    this._menuitemService.update(this.menuitem).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
