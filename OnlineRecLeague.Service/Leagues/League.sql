CREATE TABLE public.league
(
	league_id uuid NOT NULL DEFAULT uuid_generate_v1(),
	name varchar(255) COLLATE pg_catalog."default" NOT NULL,
	uri_path varchar(128) COLLATE pg_catalog."default" NOT NULL,
	game_id uuid NOT NULL,
	created_by_user_id uuid NOT NULL,
	CONSTRAINT league_pkey PRIMARY KEY (league_id),
	CONSTRAINT league_game_id_fkey FOREIGN KEY (game_id) REFERENCES public.game (game_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
) WITH ( OIDS = FALSE )
TABLESPACE pg_default;

ALTER TABLE public.league OWNER to onlinerecleague_dbuser;

GRANT ALL ON TABLE public.league TO onlinerecleague_dbuser;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.league TO onlinerecleague_dbuser;

DROP INDEX IF EXISTS public.league_game_id_idx;
CREATE INDEX league_game_id_idx ON public.league USING btree (game_id) TABLESPACE pg_default;
