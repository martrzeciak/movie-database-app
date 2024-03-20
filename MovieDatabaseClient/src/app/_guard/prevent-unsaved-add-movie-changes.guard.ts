import { CanDeactivateFn } from '@angular/router';
import { AdminAddMovieComponent } from '../admin/admin-add-movie/admin-add-movie.component';
import { inject } from '@angular/core';
import { ConfirmService } from '../_services/confirm.service';

export const preventUnsavedAddMovieChangesGuard: CanDeactivateFn<AdminAddMovieComponent> = (component) => {
  const confirmService = inject(ConfirmService);

  if (component.addForm?.dirty) {
    return confirmService.confirm();
  }

  return true;
};
