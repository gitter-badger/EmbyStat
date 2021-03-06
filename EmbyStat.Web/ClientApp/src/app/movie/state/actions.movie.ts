import { Action } from '@ngrx/store';
import { MovieStats } from '../models/movieStats';
import { MoviePersonStats } from '../models/moviePersonStats';
import { Collection } from '../../shared/models/collection';
import { MovieGraphs } from '../models/movieGraphs';
import { SuspiciousMovies } from '../models/suspiciousMovies';

export enum MovieActionTypes {
  LOAD_STATS_GENERAL = '[MovieGeneralStats] Load General Movie Stats',
  LOAD_STATS_GENERAL_SUCCESS = '[MovieGeneralStats] Load General Movie Stats Success',
  LOAD_MOVIE_COLLECTIONS = '[MovieCollections] Load Movie Collections',
  LOAD_MOVIE_COLLECTIONS_SUCCESS = '[MovieCollections] Load Movie Collections Success',
  LOAD_STATS_PERSON = '[MoviePersonStats] Load Person Movie Stats',
  LOAD_STATS_PERSON_SUCCESS = '[MoviePersonStats] Load Person Movie Stats Success',
  LOAD_SUSPICIOUS = '[MovieSuspicious] Load Suspicious',
  LOAD_SUSPICIOUS_SUCCESS = '[MovieSuspicious] Load Suspicious Success',
  LOAD_GRAPHS = '[MovieGraphs] Load Movie Graphs',
  LOAD_GRAPHS_SUCCESS = '[MovieGraphs] Load Movie Graphs Success'
}

export class LoadGeneralStatsAction implements Action {
  readonly type = MovieActionTypes.LOAD_STATS_GENERAL;
  constructor(public payload: string[]) { }
}

export class LoadGeneralStatsSuccessAction implements Action {
  readonly type = MovieActionTypes.LOAD_STATS_GENERAL_SUCCESS;
  constructor(public payload: MovieStats) { }
}

export class LoadMovieCollectionsAction implements Action {
  readonly type = MovieActionTypes.LOAD_MOVIE_COLLECTIONS;
  constructor(public payload = null) { }
}

export class LoadMovieCollectionsSuccessAction implements Action {
  readonly type = MovieActionTypes.LOAD_MOVIE_COLLECTIONS_SUCCESS;
  constructor(public payload: Collection[]) { }
}

export class LoadPersonStatsAction implements Action {
  readonly type = MovieActionTypes.LOAD_STATS_PERSON;
  constructor(public payload: string[]) { }
}

export class LoadPersonStatsSuccessAction implements Action {
  readonly type = MovieActionTypes.LOAD_STATS_PERSON_SUCCESS;
  constructor(public payload: MoviePersonStats) { }
}

export class LoadSuspiciousAction implements Action {
  readonly type = MovieActionTypes.LOAD_SUSPICIOUS;
  constructor(public payload: string[]) { }
}

export class LoadSuspiciousSuccessAction implements Action {
  readonly type = MovieActionTypes.LOAD_SUSPICIOUS_SUCCESS;
  constructor(public payload: SuspiciousMovies) { }
}

export class LoadGraphsAction implements Action {
  readonly type = MovieActionTypes.LOAD_GRAPHS;
  constructor(public payload: string[]) { }
}

export class LoadGraphsSuccessAction implements Action {
  readonly type = MovieActionTypes.LOAD_GRAPHS_SUCCESS;
  constructor(public payload: MovieGraphs) { }
}

export type MovieActions = LoadGeneralStatsAction | LoadGeneralStatsSuccessAction |
  LoadMovieCollectionsAction | LoadMovieCollectionsSuccessAction |
  LoadPersonStatsAction | LoadPersonStatsSuccessAction |
  LoadSuspiciousAction | LoadSuspiciousSuccessAction |
  LoadGraphsAction | LoadGraphsSuccessAction;
