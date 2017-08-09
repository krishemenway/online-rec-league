-- Table: svc.ladder_challenge

DROP TABLE IF EXISTS svc.ladder_challenge;

CREATE TABLE svc.ladder_challenge
(
    ladder_challenge_id uuid NOT NULL,
    challenger_team_id uuid NOT NULL,
    challenged_team_id uuid NOT NULL,
    match_results json,
    challenge_successful boolean,
    match_results_reported_time timestamp with time zone,
    ladder_id uuid NOT NULL,
    challenge_time timestamp with time zone NOT NULL,
    CONSTRAINT ladder_challenge_pkey PRIMARY KEY (ladder_challenge_id),
    CONSTRAINT ladder_challenge_challenged_team_id_fkey FOREIGN KEY (challenged_team_id)
        REFERENCES svc.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT ladder_challenge_challenger_team_id_fkey FOREIGN KEY (challenger_team_id)
        REFERENCES svc.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT ladder_challenge_ladder_id_fkey FOREIGN KEY (ladder_id)
        REFERENCES svc.ladder (ladder_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT ladder_winning_team_id_fkey FOREIGN KEY (winning_team_id)
        REFERENCES svc.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.ladder_challenge
    OWNER to postgres;

GRANT ALL ON TABLE svc.ladder_challenge TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.ladder_challenge TO leaguesweb;

-- Index: ladder_challenge_challenged_team_id_idx

DROP INDEX IF EXISTS svc.ladder_challenge_challenged_team_id_idx;

CREATE INDEX ladder_challenge_challenged_team_id_idx
    ON svc.ladder_challenge USING btree
    (challenged_team_id)
    TABLESPACE pg_default;

-- Index: ladder_challenge_challenger_team_id_idx

DROP INDEX IF EXISTS svc.ladder_challenge_challenger_team_id_idx;

CREATE INDEX ladder_challenge_challenger_team_id_idx
    ON svc.ladder_challenge USING btree
    (challenger_team_id)
    TABLESPACE pg_default;

-- Index: ladder_challenge_ladder_id_idx

DROP INDEX IF EXISTS svc.ladder_challenge_ladder_id_idx;

CREATE INDEX ladder_challenge_ladder_id_idx
    ON svc.ladder_challenge USING btree
    (ladder_id)
    TABLESPACE pg_default;

-- Index: ladder_challenge_winning_team_id_idx

DROP INDEX IF EXISTS svc.ladder_challenge_winning_team_id_idx;

CREATE INDEX ladder_challenge_winning_team_id_idx
    ON svc.ladder_challenge USING btree
    (winning_team_id)
    TABLESPACE pg_default;