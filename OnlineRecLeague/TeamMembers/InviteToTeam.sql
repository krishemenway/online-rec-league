CREATE TABLE svc.invite_to_team
(
	team_id uuid NOT NULL,
	email varchar(256) COLLATE pg_catalog."default" NOT NULL,
	created_time timestamp with time zone NOT NULL,

	CONSTRAINT team_pkey PRIMARY KEY (team_id, email),
	CONSTRAINT team_id_fkey FOREIGN KEY (team_id) REFERENCES svc.team (team_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH ( OIDS = FALSE )
TABLESPACE pg_default;

ALTER TABLE svc.invite_to_team OWNER to leaguesweb;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.invite_to_team TO leaguesweb;

CREATE INDEX team_email_idx ON svc.team USING btree (email) TABLESPACE pg_default;