-- Table: svc.game

-- DROP TABLE svc.game;

CREATE TABLE svc.game
(
    game_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    uri_path character varying(64) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT game_pkey PRIMARY KEY (game_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.game
    OWNER to postgres;

GRANT ALL ON TABLE svc.game TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.game TO leaguesweb;

-- Index: game_uri_path_uidx

-- DROP INDEX svc.game_uri_path_uidx;

CREATE UNIQUE INDEX game_uri_path_uidx
    ON svc.game USING btree
    (uri_path COLLATE pg_catalog."default")
    TABLESPACE pg_default;