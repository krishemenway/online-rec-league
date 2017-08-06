-- Table: svc.team

-- DROP TABLE svc.team;

CREATE TABLE svc.team
(
    team_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name character varying(256) COLLATE pg_catalog."default" NOT NULL,
    profile_content text COLLATE pg_catalog."default",
    user_name_prefix character varying(8) COLLATE pg_catalog."default",
    owner_user_id uuid NOT NULL,
    created_time timestamp with time zone NOT NULL,
    CONSTRAINT team_pkey PRIMARY KEY (team_id),
    CONSTRAINT owner_user_id_fkey FOREIGN KEY (owner_user_id)
        REFERENCES svc."user" (user_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.team OWNER to postgres;
GRANT ALL ON TABLE svc.team TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.team TO leaguesweb;

-- Index: team_owner_user_id_idx

DROP INDEX svc.team_owner_user_id_idx;

CREATE INDEX team_owner_user_id_idx
    ON svc.team USING btree
    (owner_user_id)
    TABLESPACE pg_default;