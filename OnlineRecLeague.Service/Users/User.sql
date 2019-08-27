CREATE TABLE svc.user
(
	user_id uuid NOT NULL DEFAULT uuid_generate_v1(),
	nickname character varying(64) COLLATE pg_catalog."default" NOT NULL,
	realname character varying(128) COLLATE pg_catalog."default" NULL,
	email character varying(256) COLLATE pg_catalog."default" NOT NULL,
	default_timezone character varying(32) COLLATE pg_catalog."default" NULL,
	password character varying(256) COLLATE pg_catalog."default" NULL,
	email_confirmation_time timestamp with time zone NULL,
	email_confirmation_token character varying(32) COLLATE pg_catalog."default" NULL,
	join_time timestamp with time zone NOT NULL,
	quit_time timestamp with time zone NULL,
	CONSTRAINT user_pkey PRIMARY KEY (user_id)
)
WITH ( OIDS = FALSE ) TABLESPACE pg_default;

ALTER TABLE svc.user OWNER to leaguesweb;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.user TO leaguesweb;

CREATE UNIQUE INDEX user_email_uidx ON svc.user USING btree (email COLLATE pg_catalog."default") TABLESPACE pg_default;
