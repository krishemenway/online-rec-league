CREATE TABLE public.team
(
	team_id uuid NOT NULL DEFAULT uuid_generate_v1(),
	name varchar(256) COLLATE pg_catalog."default" NOT NULL,
	profile_content text COLLATE pg_catalog."default",
	owner_user_id uuid NOT NULL,
	created_time timestamp with time zone NOT NULL,
	CONSTRAINT team_pkey PRIMARY KEY (team_id),
	CONSTRAINT owner_user_id_fkey FOREIGN KEY (owner_user_id) REFERENCES public."user" (user_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
) WITH (OIDS = FALSE) TABLESPACE pg_default;

ALTER TABLE public.team OWNER to onlinerecleague_dbuser;
GRANT ALL ON TABLE public.team TO onlinerecleague_dbuser;
GRANT INSERT, SELECT, UPDATE, DELETE ON TABLE public.team TO onlinerecleague_dbuser;

CREATE INDEX team_owner_user_id_idx ON public.team USING btree (owner_user_id) TABLESPACE pg_default;
