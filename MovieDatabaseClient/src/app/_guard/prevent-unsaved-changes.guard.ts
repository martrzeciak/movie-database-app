import { CanDeactivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { ConfirmService } from '../_services/confirm.service';
import { EditUserComponent } from '../users/edit-user/edit-user.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<EditUserComponent> = (component) => {
  const confirmService = inject(ConfirmService);
  
  if (component.editForm?.dirty) {
    return confirmService.confirm();
  }
  
  return true;
};