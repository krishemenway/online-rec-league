-- Table: public.ladder_challenge

-- DROP TABLE public.ladder_challenge;

CREATE TABLE public.ladder_challenge
(
    ladder_challenge_id uuid NOT NULL,
    challenger_team_id uuid NOT NULL,
    challenged_team_id uuid NOT NULL,
    match_time_offer_one timestamp with time zone NOT NULL,
    match_time_offer_two timestamp with time zone NOT NULL,
    match_time_offer_three timestamp with time zone NOT NULL,
    match_results json,
    winning_team_id uuid,
    ladder_id uuid NOT NULL,
    challenge_time timestamp with time zone NOT NULL,
    CONSTRAINT ladder_challenge_pkey PRIMARY KEY (ladder_challenge_id),
    CONSTRAINT ladder_challenge_challenged_team_id_fkey FOREIGN KEY (challenged_team_id)
        REFERENCES public.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT ladder_challenge_challenger_team_id_fkey FOREIGN KEY (challenger_team_id)
        REFERENCES public.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT ladder_challenge_ladder_id_fkey FOREIGN KEY (ladder_id)
        REFERENCES public.ladder (ladder_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT ladder_winning_team_id_fkey FOREIGN KEY (winning_team_id)
        REFERENCES public.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.ladder_challenge
    OWNER to postgres;

GRANT ALL ON TABLE public.ladder_challenge TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.ladder_challenge TO leaguesweb;

-- Index: ladder_challenge_challenged_team_id_idx

-- DROP INDEX public.ladder_challenge_challenged_team_id_idx;

CREATE INDEX ladder_challenge_challenged_team_id_idx
    ON public.ladder_challenge USING btree
    (challenged_team_id)
    TABLESPACE pg_default;

-- Index: ladder_challenge_challenger_team_id_idx

-- DROP INDEX public.ladder_challenge_challenger_team_id_idx;

CREATE INDEX ladder_challenge_challenger_team_id_idx
    ON public.ladder_challenge USING btree
    (challenger_team_id)
    TABLESPACE pg_default;

-- Index: ladder_challenge_ladder_id_idx

-- DROP INDEX public.ladder_challenge_ladder_id_idx;

CREATE INDEX ladder_challenge_ladder_id_idx
    ON public.ladder_challenge USING btree
    (ladder_id)
    TABLESPACE pg_default;

-- Index: ladder_challenge_winning_team_id_idx

-- DROP INDEX public.ladder_challenge_winning_team_id_idx;

CREATE INDEX ladder_challenge_winning_team_id_idx
    ON public.ladder_challenge USING btree
    (winning_team_id)
    TABLESPACE pg_default;