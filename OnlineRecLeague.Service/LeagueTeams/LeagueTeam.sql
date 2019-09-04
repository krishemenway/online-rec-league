CREATE TABLE public.league_team
(
	league_team_id uuid NOT NULL DEFAULT uuid_generate_v1(),
	team_id uuid NOT NULL,
	name character varying(128) COLLATE pg_catalog."default" NULL,
	created_time timestamp with time zone NOT NULL,
	created_by_user_id uuid NOT NULL,
	CONSTRAINT league_team_pkey PRIMARY KEY (league_team_id),
	CONSTRAINT league_team_id_fkey FOREIGN KEY (team_id) REFERENCES public.team (team_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
) WITH (OIDS = FALSE) TABLESPACE pg_default;

ALTER TABLE public.league_team OWNER to onlinerecleague_dbuser;
GRANT ALL ON TABLE public.league_team TO onlinerecleague_dbuser;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.league_team TO onlinerecleague_dbuser;


CREATE INDEX league_team_team_id_idx ON public.league_team USING btree (team_id) TABLESPACE pg_default;
