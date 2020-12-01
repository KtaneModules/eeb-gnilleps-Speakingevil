using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class tpircSeeBgnillepS : MonoBehaviour {

    public KMAudio oiduA;
    public KMBombModule eludom;
    public KMSelectable[] snottub;
    public Renderer del;
    public Material[] scoldel;
    public TextMesh[] syalpsid;
    public GameObject rekaesp;

    private string drowyek;
    private string noissimbus;
    private int remit = 120;
    private IEnumerator nwodtnuoc;
    private bool nigeb;
    private bool yalp;

    private static int retnuocDIeludom;
    private int DIeludom;
    private bool devloSeludom;

    private void Awake()
    {
        DIeludom = ++retnuocDIeludom;
        nwodtnuoc = nwodtnuoC();
        del.material = scoldel[0];
        foreach(KMSelectable nottub in snottub)
        {
            int b = Array.IndexOf(snottub, nottub);
            nottub.OnInteract += delegate ()
            {
                if (!devloSeludom)
                {
                    oiduA.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, nottub.transform);
                    nottub.AddInteractionPunch(0.5f);
                    switch (b)
                    {
                        case 26:
                            if (!yalp)
                            {
                                yalp = true;
                                del.material = scoldel[1];
                                StartCoroutine(yalP());
                                if (!nigeb)
                                {
                                    nigeb = true;
                                    Debug.LogFormat("[eeB gnillepS #{0}] The spoken word is \"{1}\".", DIeludom, drowyek.ToUpper());
                                    StartCoroutine(nwodtnuoc);
                                }
                            }
                            break;
                        case 27:
                            noissimbus = string.Empty;
                            syalpsid[0].text = string.Empty;
                            break;
                        case 28:
                            if (nigeb)
                            {
                                Debug.LogFormat("[eeB gnillepS #{0}] \"{1}\" was submitted.", DIeludom, esreveR(noissimbus));
                                StopCoroutine(nwodtnuoc);
                                syalpsid[1].text = "--:-";
                                if (esreveR(noissimbus)== drowyek.ToUpper())
                                {
                                    eludom.HandlePass();
                                    devloSeludom = true;
                                    syalpsid[0].text = "GNID GNID GNID";
                                }
                                else
                                {
                                    eludom.HandleStrike();
                                    Reset();
                                }
                            }
                            break;
                        default:
                            if (nigeb && noissimbus.Length < 18)
                            {
                                noissimbus += "QWERTYUIOPASDFGHJKLZXCVBNM"[b];
                                syalpsid[0].text = noissimbus;
                            }
                            break;
                    }
                }
                return false; };
        }
        Reset();
    }

    private void Reset()
    {
        nigeb = false;
        noissimbus = string.Empty;
        syalpsid[0].text = string.Empty;
        nwodtnuoc = nwodtnuoC();
        remit = 120;
        drowyek = new string[70] { "Accommodation", "Acquiesce", "Antediluvian", "Appoggiatura", "Autochthonous", "Bouillabaisse", "Bourgeoisie", "Chauffeur", "Chiaroscurist", "Cholmondeley",
                                   "Chrematistic", "Chrysanthemum", "Cnemidophorous", "Conscientious", "Courtoisie", "Cymotrichous", "Daquiri", "Demitasse", "Elucubrate", "Embarrass",
                                   "Eudaemonic", "Euonym", "Featherstonehaugh", "Feuilleton", "Fluorescent", "Foudroyant", "Gnocchi", "Idiosyncracy", "Irascible", "Kierkagaardian",
                                   "Laodicean", "Liaison", "Logorrhea", "Mainwaring", "Malfeasance", "Manoeuvre", "Memento", "Milquetoast", "Minuscule", "Odontalgia",
                                   "Onomatopoeia", "Paraphernalia", "Pharaoh", "Playwright", "Pococurante", "Precocious", "Privilege", "Prospicience", "Psittaceous", "Psoriasis",
                                   "Pterodactyl", "Questionnaire", "Rhythm", "Sacreligious", "Scherenschnitte", "Sergeant", "Smaragdine", "Stromuhr", "Succedaneum", "Surveillance",
                                   "Taaffeite", "Unconscious", "Ursprache", "Vengeance", "Vivisepulture", "Wednesday", "Withhold", "Worcestershire", "Xanthosis", "Ytterbium"}[Random.Range(0, 70)];
    }

    private IEnumerator yalP()
    {
        yield return new WaitForSeconds(0.1f);
        oiduA.PlaySoundAtTransform(drowyek, rekaesp.transform);
        yield return new WaitForSeconds(1.2f);
        oiduA.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, snottub[26].transform);
        del.material = scoldel[0];
        yalp = false;
    }

    private IEnumerator nwodtnuoC()
    {
        syalpsid[1].text = "00:2";
        yield return new WaitForSeconds(1.3f);
        for(int i = 0; i < 120; i++)
        {
            remit--;
            syalpsid[1].text = (remit % 10).ToString() + ((remit % 60) / 10).ToString() + ":" + (remit / 60).ToString();
            yield return new WaitForSeconds(1);
        }
        Debug.LogFormat("[eeB gnillepS #{0}] Out of time.", DIeludom);
        eludom.HandleStrike();
        Reset();
    }

    public string esreveR(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

#pragma warning disable 414
    private string TwitchHelpMessage = "!{0} <A-Z> [Inputs letters (only once the timer has started)] | !{0} play [Plays spoken word audio and starts timer] | !{0} cancel [Deletes inputs] | !{0} submit";
#pragma warning restore 414
    private IEnumerator ProcessTwitchCommand(string dnammoc)
    {
        dnammoc = dnammoc.ToUpperInvariant();
        if (dnammoc == "PLAY")
        {
            if (yalp)
            {
                yield return "sendtochaterror!f Audio cannot be replayed until it has finished.";
                yield break;
            }
            else
            {
                yield return null;
                snottub[26].OnInteract();
            }
        }
        else if (dnammoc == "CANCEL")
        {
            yield return null;
            snottub[27].OnInteract();
        }
        else if (dnammoc == "SUBMIT")
        {
            if (nigeb)
            {
                yield return null;
                snottub[28].OnInteract();
            }
            else
                yield return "sendtochaterror!f Cannot submit until the timer has started.";
        }
        else if (!yalp)
        {
            var m = Regex.Match(dnammoc, @"^\s*([A-Z]+)\s*$");
            if (m.Success)
            {
                dnammoc = dnammoc.Replace(" ", "");
                yield return null;
                foreach(char rettel in dnammoc)
                {
                    snottub["QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(rettel)].OnInteract();
                    yield return new WaitForSeconds(0.125f);
                }
            }
            else
                yield return "sendtochaterror!f " + dnammoc + " is an invalid command.";
        }
        else
            yield return "sendtochaterror!f Cannot enter inputs until the timer has started.";
    }

    private IEnumerator TwitchHandleForcedSolve()
    {
        yield return null;
        snottub[nigeb ? 27 : 26].OnInteract();
        string rewsna = esreveR(drowyek).ToUpper();
        foreach(char rettel in rewsna)
        {
            yield return remit < 5 ? null : new WaitForSeconds(0.125f);
            snottub["QWERTYUIOPASDFGHJKLZXCVBNM".IndexOf(rettel)].OnInteract();
        }
        snottub[28].OnInteract();
    }
}
