CREATE TABLE public.ladder
(
    ladder_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name varchar(255) COLLATE pg_catalog."default" NOT NULL,
    uri_path varchar(128) COLLATE pg_catalog."default" NOT NULL,
    sport_id uuid NOT NULL,
	created_by_user_id uuid NOT NULL,
    CONSTRAINT ladder_pkey PRIMARY KEY (ladder_id),
    CONSTRAINT ladder_game_id_fkey FOREIGN KEY (game_id)
        REFERENCES public.game (game_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
) WITH ( OIDS = FALSE )
TABLESPACE pg_default;

ALTER TABLE public.ladder OWNER to onlinerecleague_dbuser;

GRANT ALL ON TABLE public.ladder TO onlinerecleague_dbuser;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.ladder TO onlinerecleague_dbuser;

DROP INDEX IF EXISTS public.ladder_esport_id_idx;
CREATE INDEX ladder_game_id_idx ON public.ladder USING btree (sport_id) TABLESPACE pg_default;