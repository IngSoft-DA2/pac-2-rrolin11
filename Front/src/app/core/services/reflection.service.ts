import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ReflectionService {

  public reflectionPageVisitsCounter: number = 0;
  private http: HttpClient = inject(HttpClient);

  // TODO: Should implement MUTEX for reflectionPageVisitsCounter!
  public getReflectionPageVisitsCounter() {
    return this.reflectionPageVisitsCounter;
  }

  public incrementReflectionPageVisitsCounter() {
    this.reflectionPageVisitsCounter++;
    console.log("Reflection page visits counter incremented to: " + this.reflectionPageVisitsCounter);
  }

  public resetReflectionPageVisitsCounter() {
    this.reflectionPageVisitsCounter = 0;
  }

  public getDllNames(): Observable<string[] | null> {
    return new Observable(observable$ => {
      this.http.get<string[]>('/api/reflection/importers')
        .subscribe({
          next: (response) => {
            observable$.next(response);
          },
          error: (err) => {
            console.log(err);
            observable$.next(null);
          }
        })
    })
  }


}
