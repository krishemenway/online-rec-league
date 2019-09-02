CREATE TABLE svc.game
(
    game_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name varchar(255) COLLATE pg_catalog."default" NOT NULL,
    uri_path varchar(64) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT game_pkey PRIMARY KEY (game_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.game
    OWNER to onlinerecleague_dbuser;

GRANT ALL ON TABLE svc.game TO onlinerecleague_dbuser;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.game TO onlinerecleague_dbuser;

-- Index: game_uri_path_uidx

DROP INDEX IF EXISTS svc.game_uri_path_uidx;

CREATE UNIQUE INDEX game_uri_path_uidx
    ON svc.game USING btree
    (uri_path COLLATE pg_catalog."default")
    TABLESPACE pg_default;