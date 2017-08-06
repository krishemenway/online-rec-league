-- Table: svc.esport

-- DROP TABLE svc.esport;

CREATE TABLE svc.esport
(
    esport_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name character varying(255) COLLATE pg_catalog."default" NOT NULL,
    game_id uuid NOT NULL,
    configured_rules jsonb,
    CONSTRAINT esport_pkey PRIMARY KEY (esport_id),
    CONSTRAINT esport_game_id_fkey FOREIGN KEY (game_id)
        REFERENCES svc.game (game_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.esport
    OWNER to postgres;

GRANT ALL ON TABLE svc.esport TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.esport TO leaguesweb;

-- Index: esport_game_id_idx

-- DROP INDEX svc.esport_game_id_idx;

CREATE INDEX esport_game_id_idx
    ON svc.esport USING btree
    (game_id)
    TABLESPACE pg_default;