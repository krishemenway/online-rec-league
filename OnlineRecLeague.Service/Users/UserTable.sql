-- Table: svc."user"

DROP TABLE IF EXISTS svc."user";

CREATE TABLE svc."user"
(
    user_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    nickname character varying(64) COLLATE pg_catalog."default" NOT NULL,
    realname character varying(128) COLLATE pg_catalog."default",
    email character varying(256) COLLATE pg_catalog."default" NOT NULL,
	password character varying(256) COLLATE pg_catalog."default" NOT NULL,
    default_timezone character varying(32) COLLATE pg_catalog."default",
    email_validated boolean NOT NULL,
    join_time timestamp with time zone NOT NULL,
    quit_time timestamp with time zone,
    CONSTRAINT user_pkey PRIMARY KEY (user_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc."user" OWNER to leaguesweb;
GRANT ALL ON TABLE svc."user" TO leaguesweb;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc."user" TO leaguesweb;

-- Index: user_email_uidx

DROP INDEX IF EXISTS svc.user_email_uidx;

CREATE UNIQUE INDEX user_email_uidx
    ON svc."user" USING btree
    (email COLLATE pg_catalog."default")
    TABLESPACE pg_default;