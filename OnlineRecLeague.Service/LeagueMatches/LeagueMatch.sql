CREATE TABLE public.league_match
(
	league_match_id uuid NOT NULL DEFAULT uuid_generate_v1(),
	league_id uuid NOT NULL,

	match_created_time timestamp with time zone NOT NULL,
	match_start_time timestamp with time zone NOT NULL,
	match_finish_time timestamp with time zone NOT NULL,

	home_match_team_id uuid NOT NULL,
	away_match_team_id uuid NOT NULL,

	winning_league_team_id uuid,

	match_results json,
	match_results_reported_time timestamp with time zone,

	CONSTRAINT league_match_pkey PRIMARY KEY (league_match_id),
	CONSTRAINT league_match_home_match_team_id_fkey FOREIGN KEY (home_match_team_id) REFERENCES public.league_team (league_team_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT league_match_away_match_team_id_fkey FOREIGN KEY (away_match_team_id) REFERENCES public.league_team (league_team_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT league_match_league_id_fkey FOREIGN KEY (league_id) REFERENCES public.league (league_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
) WITH ( OIDS = FALSE ) TABLESPACE pg_default;

ALTER TABLE public.league_match OWNER to onlinerecleague_dbuser;
GRANT ALL ON TABLE public.league_match TO onlinerecleague_dbuser;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.league_match TO onlinerecleague_dbuser;

CREATE INDEX league_match_home_match_team_id_idx ON public.league_match USING btree (home_match_team_id) TABLESPACE pg_default;
CREATE INDEX league_match_away_match_team_id_idx ON public.league_match USING btree (away_match_team_id) TABLESPACE pg_default;
CREATE INDEX league_match_league_id_idx ON public.league_match USING btree (league_id) TABLESPACE pg_default;
