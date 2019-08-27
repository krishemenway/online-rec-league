DROP TABLE IF EXISTS svc.ladder;

CREATE TABLE svc.ladder
(
    ladder_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name varchar(255) COLLATE pg_catalog."default" NOT NULL,
    uri_path varchar(128) COLLATE pg_catalog."default" NOT NULL,
    sport_id uuid NOT NULL,
    CONSTRAINT ladder_pkey PRIMARY KEY (ladder_id),
    CONSTRAINT ladder_game_id_fkey FOREIGN KEY (game_id)
        REFERENCES svc.game (game_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
) WITH ( OIDS = FALSE )
TABLESPACE pg_default;

ALTER TABLE svc.ladder OWNER to leaguesweb;

GRANT ALL ON TABLE svc.ladder TO leaguesweb;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.ladder TO leaguesweb;

DROP INDEX IF EXISTS svc.ladder_esport_id_idx;
CREATE INDEX ladder_game_id_idx ON svc.ladder USING btree (sport_id) TABLESPACE pg_default;