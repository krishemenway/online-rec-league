CREATE TABLE svc.league
(
	league_id uuid NOT NULL DEFAULT uuid_generate_v1(),
	name varchar(255) COLLATE pg_catalog."default" NOT NULL,
	uri_path varchar(128) COLLATE pg_catalog."default" NOT NULL,
	rules text COLLATE pg_catalog."default" NOT NULL,
	game_id uuid NOT NULL,

	CONSTRAINT league_pkey PRIMARY KEY (league_id),
	CONSTRAINT league_game_id_fkey FOREIGN KEY (game_id) REFERENCES svc.game (game_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
)
WITH ( OIDS = FALSE ) TABLESPACE pg_default;

ALTER TABLE svc.league OWNER to leaguesweb;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.league TO leaguesweb;

CREATE INDEX league_game_id_idx ON svc.league USING btree (game_id) TABLESPACE pg_default;