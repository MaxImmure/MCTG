	DROP TABLE package;
	DROP TABLE public."admin";
	DROP TABLE userstats;
	DROP TABLE login_credentials;
	DROP TABLE deck;
	DROP TABLE card;
	DROP TABLE public."user";

	CREATE TABLE IF NOT EXISTS public."user"
	(
		guid text COLLATE pg_catalog."default" NOT NULL,
		name text COLLATE pg_catalog."default" DEFAULT '', 
		description text COLLATE pg_catalog."default",
		coins integer NOT NULL DEFAULT 20,
		CONSTRAINT user_pkey PRIMARY KEY (guid),
		CONSTRAINT user_guid_key UNIQUE (guid)
	)

	TABLESPACE pg_default;

	ALTER TABLE IF EXISTS public."user"
		OWNER to swe1user;

	-- Table: public.card

	-- DROP TABLE IF EXISTS public.card;

	CREATE TABLE IF NOT EXISTS public.card
	(
		cardid text COLLATE pg_catalog."default" NOT NULL,
		ownerid text COLLATE pg_catalog."default" NOT NULL,
		element text COLLATE pg_catalog."default" NOT NULL,
		type text COLLATE pg_catalog."default" NOT NULL,
		cardname text COLLATE pg_catalog."default",
		damage double precision NOT NULL DEFAULT 0.0,
		CONSTRAINT card_pkey PRIMARY KEY (cardid, ownerid),
		CONSTRAINT card_cardid_key UNIQUE (cardid),
		CONSTRAINT card_ownerid_fkey FOREIGN KEY (ownerid)
			REFERENCES public."user" (guid) MATCH SIMPLE
			ON UPDATE NO ACTION
			ON DELETE NO ACTION
	)

	TABLESPACE pg_default;

	ALTER TABLE IF EXISTS public.card
		OWNER to swe1user;

	-- Table: public.deck

	-- DROP TABLE IF EXISTS public.deck;

	CREATE TABLE IF NOT EXISTS public.deck
	(
		cardid text COLLATE pg_catalog."default" NOT NULL,
		ownerid text COLLATE pg_catalog."default" NOT NULL,
		CONSTRAINT deck_pkey PRIMARY KEY (cardid, ownerid),
		CONSTRAINT deck_cardid_ownerid_fkey FOREIGN KEY (cardid, ownerid)
			REFERENCES public.card (cardid, ownerid) MATCH SIMPLE
			ON UPDATE NO ACTION
			ON DELETE NO ACTION
	)

	TABLESPACE pg_default;

	ALTER TABLE IF EXISTS public.deck
		OWNER to swe1user;

	-- Table: public.login_credentials

	-- DROP TABLE IF EXISTS public.login_credentials;

	CREATE TABLE IF NOT EXISTS public.login_credentials
	(
		guid text COLLATE pg_catalog."default" NOT NULL,
		username text COLLATE pg_catalog."default" NOT NULL,
		password text COLLATE pg_catalog."default" NOT NULL,
		CONSTRAINT login_credentials_pkey PRIMARY KEY (guid),
		CONSTRAINT login_credentials_guid_key UNIQUE (guid),
		CONSTRAINT guid FOREIGN KEY (guid)
			REFERENCES public."user" (guid) MATCH SIMPLE
			ON UPDATE NO ACTION
			ON DELETE NO ACTION
	)

	TABLESPACE pg_default;

	ALTER TABLE IF EXISTS public.login_credentials
		OWNER to swe1user;

	-- Table: public.userstats

	-- DROP TABLE IF EXISTS public.userstats;

	CREATE TABLE IF NOT EXISTS public.userstats
	(
		guid text COLLATE pg_catalog."default" NOT NULL,
		win integer NOT NULL DEFAULT 0,
		lose integer NOT NULL DEFAULT 0,
		draw integer NOT NULL DEFAULT 0,
		elo integer NOT NULL DEFAULT 100,
		CONSTRAINT userstats_pkey PRIMARY KEY (guid),
		CONSTRAINT guid FOREIGN KEY (guid)
			REFERENCES public."user" (guid) MATCH SIMPLE
			ON UPDATE NO ACTION
			ON DELETE NO ACTION
	)

	TABLESPACE pg_default;

	ALTER TABLE IF EXISTS public.userstats
		OWNER to swe1user;

	CREATE TABLE IF NOT EXISTS public.admin
	(
		guid text COLLATE pg_catalog."default" NOT NULL,
		CONSTRAINT admin_pkey PRIMARY KEY (guid),
		CONSTRAINT guid FOREIGN KEY (guid)
			REFERENCES public."user" (guid) MATCH SIMPLE
			ON UPDATE NO ACTION
			ON DELETE NO ACTION
	)

	TABLESPACE pg_default;

	ALTER TABLE IF EXISTS public.admin
		OWNER to swe1user;
		
		-- Table: public.package

-- DROP TABLE IF EXISTS public."package";

CREATE TABLE IF NOT EXISTS public."package"
(
	ind SERIAL NOT NULL,
	p_id text COLLATE pg_catalog."default" NOT NULL,
    cardid text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT ind PRIMARY KEY (ind),
    CONSTRAINT cardid FOREIGN KEY (cardid)
        REFERENCES public.card (cardid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."package"
    OWNER to swe1user;
	
INSERT INTO public."user" (guid, description)
  VALUES ('00000000-0000-0000-0000-000000000000', 'package');