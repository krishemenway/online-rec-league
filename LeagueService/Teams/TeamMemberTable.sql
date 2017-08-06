-- Table: svc.team_member

-- DROP TABLE svc.team_member;

CREATE TABLE svc.team_member
(
    team_member_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    team_id uuid NOT NULL,
    user_id uuid NOT NULL,
    nickname character varying(64) COLLATE pg_catalog."default",
    join_time time with time zone,
    CONSTRAINT team_member_pkey PRIMARY KEY (team_member_id),
    CONSTRAINT team_member_team_id_fkey FOREIGN KEY (team_id)
        REFERENCES svc.team (team_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT team_member_user_id_fkey FOREIGN KEY (user_id)
        REFERENCES svc."user" (user_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.team_member
    OWNER to postgres;

GRANT ALL ON TABLE svc.team_member TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.team_member TO leaguesweb;

-- Index: team_member_team_id_idx

DROP INDEX svc.team_member_team_id_idx;

CREATE INDEX team_member_team_id_idx
    ON svc.team_member USING btree
    (team_id)
    TABLESPACE pg_default;

-- Index: team_member_user_id_idx

DROP INDEX svc.team_member_user_id_idx;

CREATE INDEX team_member_user_id_idx
    ON svc.team_member USING btree
    (user_id)
    TABLESPACE pg_default;