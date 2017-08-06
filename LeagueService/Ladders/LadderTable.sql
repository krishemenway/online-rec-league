-- Table: svc.ladder

-- DROP TABLE svc.ladder;

CREATE TABLE svc.ladder
(
    ladder_id uuid NOT NULL DEFAULT uuid_generate_v1(),
    name character varying(255)[] COLLATE pg_catalog."default" NOT NULL,
    uri_path character varying(128)[] COLLATE pg_catalog."default" NOT NULL,
    force_real_names boolean NOT NULL,
    esport_id uuid NOT NULL,
    CONSTRAINT ladder_pkey PRIMARY KEY (ladder_id),
    CONSTRAINT ladder_esport_id_fkey FOREIGN KEY (esport_id)
        REFERENCES svc.esport (esport_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE svc.ladder
    OWNER to postgres;

GRANT ALL ON TABLE svc.ladder TO postgres;

GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE svc.ladder TO leaguesweb;

-- Index: ladder_esport_id_idx

-- DROP INDEX svc.ladder_esport_id_idx;

CREATE INDEX ladder_esport_id_idx
    ON svc.ladder USING btree
    (esport_id)
    TABLESPACE pg_default;