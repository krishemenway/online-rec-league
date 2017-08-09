-- Table: svc.ladder_team

-- DROP TABLE svc.ladder_team;

CREATE TABLE svc.ladder_team
(
    ladder_team_id uuid NOT NULL,
    team_id uuid NOT NULL,
    join_time time without time zone NOT NULL,
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
    OWNER to postgres;

GRANT ALL ON TABLE svc.ladder_team TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.ladder_team TO leaguesweb;

-- Index: ladder_team_team_id_idx

-- DROP INDEX svc.ladder_team_team_id_idx;

CREATE INDEX ladder_team_team_id_idx
    ON svc.ladder_team USING btree
    (team_id)
    TABLESPACE pg_default;