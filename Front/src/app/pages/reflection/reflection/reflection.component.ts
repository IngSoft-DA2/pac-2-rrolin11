import { Component, inject } from '@angular/core';
import { ReflectionService } from '../../../core/services/reflection.service';

@Component({
  selector: 'app-reflection',
  imports: [],
  templateUrl: './reflection.component.html',
  styleUrl: './reflection.component.css'
})
export class ReflectionComponent {
  
  public visitsCounter: number = 0;

  
  // inject the singleton service instance
  private reflectionService = inject(ReflectionService);
  public dllNames: string[] | null = null;

  // make ngOnInit async and use the injected instance
  public ngOnInit() {
    this.reflectionService.incrementReflectionPageVisitsCounter();
    this.visitsCounter = this.reflectionService.getReflectionPageVisitsCounter();
  }

  public onButtonClick() {
    this.reflectionService.getDllNames().subscribe({
      next: (response) => {
        this.dllNames = response;
      },
      error: (err) => {
        alert("Error fetching DLL names: " + err);
      }
    });
  }

  public printDllNames(): string {
    if (this.dllNames === null) {
      return "No DLL names fetched.";
    }
    return this.dllNames.join(', ');
  }
  
}
