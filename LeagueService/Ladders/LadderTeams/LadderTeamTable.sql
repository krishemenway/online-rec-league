-- Table: svc.ladder_team

DROP TABLE IF EXISTS svc.ladder_team;

CREATE TABLE svc.ladder_team
(
    ladder_team_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    team_id uuid NOT NULL,
    join_time timestamp with time zone NOT NULL,
    current_rung bigint,
    CONSTRAINT ladder_team_pkey PRIMARY KEY (ladder_team_id),
    CONSTRAINT ladder_team_id_fkey FOREIGN KEY (team_id)
        REFERENCES svc.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.ladder_team
    OWNER to leaguesweb;

GRANT ALL ON TABLE svc.ladder_team TO leaguesweb;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.ladder_team TO leaguesweb;

-- Index: ladder_team_team_id_idx

DROP INDEX IF EXISTS svc.ladder_team_team_id_idx;

CREATE INDEX ladder_team_team_id_idx
    ON svc.ladder_team USING btree
    (team_id)
    TABLESPACE pg_default;