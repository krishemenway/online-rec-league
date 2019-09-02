CREATE TABLE svc.game_server
(
    game_server_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name character varying(128) COLLATE pg_catalog."default" NOT NULL,
    server_ip inet NOT NULL,
    owning_team_id uuid,
    CONSTRAINT game_server_pkey PRIMARY KEY (game_server_id),
    CONSTRAINT game_server_owning_team_id_fkey FOREIGN KEY (owning_team_id)
        REFERENCES public.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.game_server
    OWNER to postgres;

GRANT ALL ON TABLE public.game_server TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.game_server TO onlinerecleague_dbuser;

-- Index: game_server_owning_team_id_idx

-- DROP INDEX public.game_server_owning_team_id_idx;

CREATE INDEX game_server_owning_team_id_idx
    ON public.game_server USING btree
    (owning_team_id)
    TABLESPACE pg_default;