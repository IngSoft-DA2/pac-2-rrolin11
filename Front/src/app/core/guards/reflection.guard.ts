import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ReflectionService } from '../services/reflection.service';

export const reflectionGuard: CanActivateFn = (route, state) => {
  const router: Router = inject(Router);
  const reflectionService: ReflectionService = inject(ReflectionService);
  if (reflectionService.getReflectionPageVisitsCounter() > 20) {
    alert("Las visitas superan el límite permitido (20). Será redirigido a la consigna.");
    router.navigate(['/consigna']);
  }
  return true;
};
