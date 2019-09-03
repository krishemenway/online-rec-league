CREATE TABLE public.ladder_team
(
    ladder_team_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    team_id uuid NOT NULL,
    join_time timestamp with time zone NOT NULL,
    current_rung bigint,
    CONSTRAINT ladder_team_pkey PRIMARY KEY (ladder_team_id),
    CONSTRAINT ladder_team_id_fkey FOREIGN KEY (team_id)
        REFERENCES public.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.ladder_team
    OWNER to onlinerecleague_dbuser;

GRANT ALL ON TABLE public.ladder_team TO onlinerecleague_dbuser;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.ladder_team TO onlinerecleague_dbuser;

-- Index: ladder_team_team_id_idx

DROP INDEX IF EXISTS public.ladder_team_team_id_idx;

CREATE INDEX ladder_team_team_id_idx
    ON public.ladder_team USING btree
    (team_id)
    TABLESPACE pg_default;